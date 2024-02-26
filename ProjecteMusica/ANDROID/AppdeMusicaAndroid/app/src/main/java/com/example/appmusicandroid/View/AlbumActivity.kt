package com.example.appmusicandroid.View

import android.annotation.SuppressLint
import android.content.Intent
import android.graphics.BitmapFactory
import android.os.Bundle
import android.widget.ImageView
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.appmusicandroid.Adaper.AlbumAdapter
import com.example.appmusicandroid.Api.MongoService
import com.example.appmusicandroid.Model.Album
import com.example.appmusicandroid.databinding.AlbumListActivityBinding
import kotlinx.coroutines.launch
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.io.BufferedReader
import java.io.IOException
import java.io.InputStream
import java.io.InputStreamReader


class AlbumActivity : AppCompatActivity(){
    private var AlbumList: MutableList<Album> = mutableListOf()
    private lateinit var binding: AlbumListActivityBinding
    private lateinit var adapter: AlbumAdapter
    private lateinit var recyclerView: RecyclerView
    // API URL
    private val URL_API : String = "http://172.23.2.141:5180/"
    val retrofit = Retrofit.Builder()
        .baseUrl(URL_API)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
    private val mongoService: MongoService = retrofit.create(MongoService::class.java)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = AlbumListActivityBinding.inflate(layoutInflater)
        setContentView(binding.root)
        recyclerView = binding.recyclerViewAlbums
        NavegationFunctions()
        lifecycleScope.launch {
            GetAllAlbumsFromCloud()
            PutAlbumsToRecicleView()
        }
    }
    // Funcio que posa els albums al recycleview
    fun PutAlbumsToRecicleView() {
        recyclerView.layoutManager = LinearLayoutManager(this@AlbumActivity)
        recyclerView.adapter = AlbumAdapter(AlbumList)
    }
    // Funcio que executa la peticio de get dels albums
    fun GetAllAlbumsFromCloud() {
        val call: Call<List<Album>> = mongoService.getAllAlbums()
        call.enqueue(object : Callback<List<Album>> {
            override fun onResponse(call: Call<List<Album>>, response: Response<List<Album>>) {
                if (response.isSuccessful) {
                    val albumList = response.body()
                    if (albumList != null) {
                        for (i in albumList.indices) {
                            val albumName = albumList[i].name
                            val imageID = albumList[i].frontCoverID.toString()
                            // Fer patició amb ID del ficher de la image per cargar.
                            mongoService.getFrontCover(imageID).enqueue(object : Callback<ResponseBody> {
                                @SuppressLint("NotifyDataSetChanged")
                                override fun onResponse(call: Call<ResponseBody>, imageResponse: Response<ResponseBody>) {
                                    if (imageResponse.isSuccessful) {
                                        val image = imageResponse.body()?.byteStream()
                                        if (image != null) {
                                            val bitmap = BitmapFactory.decodeStream(imageResponse.body()?.byteStream())
                                            val albumItem = Album(albumName,2020, imageID, bitmap)
                                            AlbumList.add(albumItem)
                                            recyclerView.adapter?.notifyDataSetChanged()
                                        }
                                    }
                                }
                                override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
                                    t.printStackTrace()
                                }
                            })

                        }
                    }
                }
            }

            override fun onFailure(call: Call<List<Album>>, t: Throwable) {
                t.printStackTrace()
            }
        })

    }
    fun convertInputStreamToString(inputStream: InputStream): String {
        val reader = BufferedReader(InputStreamReader(inputStream, Charsets.UTF_8))
        val stringBuilder = StringBuilder()
        var line: String?

        try {
            while (reader.readLine().also { line = it } != null) {
                stringBuilder.append(line)
            }
        } catch (e: IOException) {
            e.printStackTrace()
        }

        return stringBuilder.toString()
    }
    // Funcio que agrega un eventlistener a cada albumsp
    fun NavegationFunctions() {
        binding.Albums.setOnClickListener{
            val intent = Intent(this@AlbumActivity, AlbumActivity::class.java)
            startActivity(intent)
        }
        binding.Home.setOnClickListener {
            val intent = Intent(this@AlbumActivity, CurrentActivity::class.java)
            startActivity(intent)
        }

        binding.Playlists.setOnClickListener {
            val intent = Intent(this@AlbumActivity, Playlist::class.java)
            startActivity(intent)
        }

        binding.CrearCanco.setOnClickListener {
            val intent = Intent(this@AlbumActivity, UploadSongActivity::class.java)
            startActivity(intent)
        }

    }
}