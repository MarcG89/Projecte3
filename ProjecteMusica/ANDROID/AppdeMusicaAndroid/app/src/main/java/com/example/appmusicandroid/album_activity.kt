package com.example.appmusicandroid
import android.graphics.BitmapFactory
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageView
import android.widget.Toast
import com.example.appmusicandroid.Api.AlbumServiceSQL
import com.example.appmusicandroid.Api.MongoService
import com.example.appmusicandroid.Model.Album
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class album_activity : AppCompatActivity() {

    private val URL_API : String = "http://172.23.2.141:5095/"

    private val retrofit = Retrofit.Builder()
        .baseUrl(URL_API)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
    private val albumServiceSQL: AlbumServiceSQL = retrofit.create(AlbumServiceSQL::class.java)
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_album)


        val name = intent.extras?.getString("Name")
        getFrontCover()
        getAlbumByNameAndYear(name.toString())
    }
    private fun getFrontCover(){
        val retrofit2 = Retrofit.Builder()
            .baseUrl("http://172.23.2.141:5180/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()
        val mongoService: MongoService = retrofit2.create(MongoService::class.java)

        val frontimg = findViewById<ImageView>(R.id.frontCover)
        val frontID = intent.extras?.getString("FrontCover")
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
                // Manejar errores de red u otros errores aquí
            }
        })

    }
    private fun getAlbumByNameAndYear(title: String) {
        albumServiceSQL.getAlbum(title).enqueue(object : Callback<Album> {
            override fun onResponse(call: Call<Album>, response: Response<Album>) {
                if (response.isSuccessful) {
                    val album = response.body()
                    if (album != null) {
                        // posar el nom de les cançons
                    } else {
                    }
                } else {
                }
            }

            override fun onFailure(call: Call<Album>, t: Throwable) {
            }
        })
    }

}
