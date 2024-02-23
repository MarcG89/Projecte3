package com.example.appmusicandroid.View

import android.content.Intent
import android.os.Bundle
import android.widget.ImageView
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.RecyclerView
import com.example.appmusicandroid.Model.Album
import com.example.appmusicandroid.databinding.AlbumActivityBinding


class AlbumActivity : AppCompatActivity(){
    private lateinit var AlbumList: MutableList<Album>
    private lateinit var binding: AlbumActivityBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = AlbumActivityBinding.inflate(layoutInflater)
        setContentView(binding.root)
        NavegationFunctions()
    }
    // Funcio que posa els albums al recycleview
    fun PutAlbumsToRecicleView() {
        val recyclerView: RecyclerView = binding.recyclerViewAlbums
        
    }
    // Funcio que executa la peticio de get dels albums
    fun GetAlbumsFromCloud() {
    }
    // Funcio que agrega un eventlistener a cada album
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