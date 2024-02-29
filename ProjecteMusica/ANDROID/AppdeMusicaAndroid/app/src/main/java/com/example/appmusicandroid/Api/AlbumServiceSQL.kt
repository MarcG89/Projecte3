package com.example.appmusicandroid.Api

import com.example.appmusicandroid.Model.Album
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query

interface AlbumServiceSQL {
    @GET("/api/Album/{title}/songs")
    fun getAlbum(
        @Path("title") title: String): Call<Album>
}
