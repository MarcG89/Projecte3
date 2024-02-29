package com.example.appmusicandroid.Api

import com.example.appmusicandroid.Model.Album
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Response
import retrofit2.http.*

// FALTA BACK COVER ID Y PODEMOS HACER PETICION POR NOMBRE
interface MongoService {
    @GET("api/v1/Album")
    fun getAllAlbums(): Call<List<Album>>
    @GET("api/v1/Album/getFrontCover/{frontCoverId}")
    fun getFrontCover(@Path("frontCoverId") frontCoverId: String): Call<ResponseBody>
    @GET("api/v1/Album/getBackCover/{backCoverId}")
    fun getBackCover(@Path("backCoverId") backCoverId: String): Call<ResponseBody>
}