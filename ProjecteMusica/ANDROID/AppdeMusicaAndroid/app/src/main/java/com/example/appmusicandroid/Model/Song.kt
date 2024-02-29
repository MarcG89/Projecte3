package com.example.appmusicandroid.Model

import android.graphics.Bitmap
import com.google.gson.annotations.SerializedName

data class Song(
    @SerializedName("\$id") val id : Int,
    @SerializedName("\$values") val values : List<SongSql>
)

data class SongSql(
    @SerializedName("title") val title: String,
)


data class Audio(
    @SerializedName("Id") val id: String?,
    @SerializedName("IdSql") val idSql: String,
    @SerializedName("Name") val name: String,
    @SerializedName("Content") val content: ByteArray?,
    @SerializedName("FrontCover") val FrontCover: Bitmap?,
    @SerializedName("BackCover") val BackCover: Bitmap?
)


