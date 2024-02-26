package com.example.appmusicandroid.Model

import android.graphics.Bitmap

data class Album (
    val Name : String? = null,
    val Artist : String? = null,
    val FrontCover: Bitmap? = null,
    val BackCover: Bitmap? = null
)
