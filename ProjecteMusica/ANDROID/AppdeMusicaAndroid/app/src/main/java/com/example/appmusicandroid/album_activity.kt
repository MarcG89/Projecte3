package com.example.appmusicandroid

import android.content.Intent
import android.graphics.BitmapFactory
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.appmusicandroid.Adaper.AlbumAdapter
import com.example.appmusicandroid.Adaper.SongAlbumAdapter
import com.example.appmusicandroid.Api.AlbumServiceSQL
import com.example.appmusicandroid.Api.MongoService
import com.example.appmusicandroid.Model.Album
import com.example.appmusicandroid.Model.Song
import com.example.appmusicandroid.Model.SongSql
import com.example.appmusicandroid.Music.FindMusic
import com.example.appmusicandroid.Music.IFindMusic
import com.example.appmusicandroid.View.AlbumActivity
import com.example.appmusicandroid.View.CloudMusicActivity
import com.example.appmusicandroid.View.CurrentActivity
import com.example.appmusicandroid.View.Playlist
import com.example.appmusicandroid.View.UploadSongActivity
import com.example.appmusicandroid.ViewModel.CurrentViewModel
import com.example.appmusicandroid.databinding.ActivityAlbumBinding
import com.google.gson.Gson
import kotlinx.coroutines.launch
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class album_activity : AppCompatActivity() {
    private lateinit var recyclerView: RecyclerView
    private var SongList: MutableList<SongSql> = mutableListOf()
    private lateinit var findMusic: IFindMusic

    private val URL_API: String = "http://172.23.2.141:5095/"
    private lateinit var binding: ActivityAlbumBinding

    private val retrofit = Retrofit.Builder()
        .baseUrl(URL_API)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
    private val albumServiceSQL: AlbumServiceSQL = retrofit.create(AlbumServiceSQL::class.java)

    private val viewModel by lazy {
        ViewModelProvider(this, defaultViewModelProviderFactory).get(CurrentViewModel::class.java)
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAlbumBinding.inflate(layoutInflater)
        setContentView(binding.root)

        recyclerView = binding.songList

        val name = intent.extras?.getString("Name")
        val year = intent.extras?.getString("Year")

        binding.year.text = year
        binding.albumName.text = name

        initListener()
        getAndSetCovers()
        PutAlbumsToRecicleView()
        getAlbumByNameAndYearTest(name.toString())
    }
    fun initListener() {
        binding.ListCloud.setOnClickListener {
            val intent = Intent(this@album_activity, CloudMusicActivity::class.java)
            startActivity(intent)
        }

        binding.Playlists.setOnClickListener {
            val intent = Intent(this@album_activity, Playlist::class.java)
            startActivity(intent)
        }

        binding.CrearCanco.setOnClickListener {
            val intent = Intent(this@album_activity, UploadSongActivity::class.java)
            startActivity(intent)
        }
        binding.Albums.setOnClickListener{
            val intent = Intent(this@album_activity, AlbumActivity::class.java)
            startActivity(intent)
        }
    }
    private fun getAndSetCovers() {
        val retrofit2 = Retrofit.Builder()
            .baseUrl("http://172.23.2.141:5180/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()
        val mongoService: MongoService = retrofit2.create(MongoService::class.java)

        val frontimg = binding.frontCover
        val backimg = binding.backCover

        val frontID = intent.extras?.getString("FrontCover")
        val backID = intent.extras?.getString("BackCover")
        mongoService.getFrontCover(frontID.toString()).enqueue(object : Callback<ResponseBody> {
            override fun onResponse(call: Call<ResponseBody>, imageResponse: Response<ResponseBody>) {
                if (imageResponse.isSuccessful) {
                    val image = imageResponse.body()?.byteStream()
                    if (image != null) {
                        val bitmap = BitmapFactory.decodeStream(imageResponse.body()?.byteStream())
                        frontimg.setImageBitmap(bitmap)
                    }
                }
            }

            override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
            }
        })
        mongoService.getBackCover(backID.toString()).enqueue(object : Callback<ResponseBody> {
            override fun onResponse(call: Call<ResponseBody>, imageResponse: Response<ResponseBody>) {
                if (imageResponse.isSuccessful) {
                    val image = imageResponse.body()?.byteStream()
                    if (image != null) {
                        val bitmap = BitmapFactory.decodeStream(imageResponse.body()?.byteStream())
                        backimg.setImageBitmap(bitmap)
                    }
                }
            }

            override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
            }
        })
    }
    fun PutAlbumsToRecicleView() {
        recyclerView.layoutManager = LinearLayoutManager(this@album_activity)
        recyclerView.adapter = SongAlbumAdapter(SongList)
    }

    private fun getAlbumByNameAndYearTest(title: String) {
        try {
            albumServiceSQL.getAlbum(title).enqueue(object : Callback<Song> {
                override fun onResponse(call: Call<Song> , response: Response<Song> ) {
                    if (response.isSuccessful) {
                        val song = response.body()
                        if (song != null) {
                            for (i in song.values) {
                                val songItem = SongSql(i.title)
                                SongList.add(songItem)
                                recyclerView.adapter?.notifyDataSetChanged()
                            }
                        }
                    } else {
                        Log.e("getAlbumByNameAndYear", "Error: ${response.errorBody()}")
                    }
                }
                override fun onFailure(call: Call<Song> , t: Throwable) {
                    Log.e("getAlbumByNameAndYear", "Error de red: ${t.message}")
                }
            })
        } catch (e: Exception) {
            Log.e("getAlbumByNameAndYear", "Error: ${e.message}")
        }
    }
}
