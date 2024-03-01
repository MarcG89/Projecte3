package com.example.appmusicandroid.Model

import android.graphics.Bitmap
import com.google.gson.annotations.SerializedName

data class Album (
    val name: String,
    val year: Int,
    @SerializedName("frontCover") val frontCoverID: String? = null,
    val frontCoverImage : Bitmap?,
    @SerializedName("backCover") val  backCoverID: String? = null,
    val backCoverImage : Bitmap? = null,
)

