package com.example.appmusicandroid.Api

import com.example.appmusicandroid.Model.Song
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Path

interface AlbumServiceSQL {
    @GET("/api/Album/{title}/songs")
    fun getAlbum(
        @Path("title") title: String): Call<Song>



    @GET("/api/Album/{title}/song")
    fun getAlbumd(
        @Path("title") title: String): Call<Song>
}
