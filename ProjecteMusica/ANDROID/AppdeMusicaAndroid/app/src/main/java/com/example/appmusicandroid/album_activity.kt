package com.example.appmusicandroid

import android.graphics.BitmapFactory
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageView
import android.widget.TextView
import com.example.appmusicandroid.Api.AlbumServiceSQL
import com.example.appmusicandroid.Api.MongoService
import com.example.appmusicandroid.Model.Album
import com.example.appmusicandroid.databinding.ActivityAlbumBinding
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class album_activity : AppCompatActivity() {

    private val URL_API: String = "http://172.23.2.141:5095/"
    private lateinit var binding: ActivityAlbumBinding

    private val retrofit = Retrofit.Builder()
        .baseUrl(URL_API)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
    private val albumServiceSQL: AlbumServiceSQL = retrofit.create(AlbumServiceSQL::class.java)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAlbumBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val name = intent.extras?.getString("Name")
        binding.albumName.text = name
        getAndSetCovers()

    }

    private fun getAndSetCovers() {
        val retrofit2 = Retrofit.Builder()
            .baseUrl("http://192.168.1.60:5180/")
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
}
