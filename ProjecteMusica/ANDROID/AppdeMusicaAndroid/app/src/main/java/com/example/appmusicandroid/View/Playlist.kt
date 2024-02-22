package com.example.appmusicandroid.View

import android.app.AlertDialog
import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.widget.EditText
import android.widget.LinearLayout
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.appmusicandroid.Adaper.PlaylistAdapter
import com.example.appmusicandroid.R
import com.example.appmusicandroid.databinding.ActivityCurrentBinding
import com.example.appmusicandroid.databinding.ActivityPlaylistBinding

class Playlist : AppCompatActivity() {

    data class Playlist(val name: String, val artist: String)

    private val playlists = mutableListOf(
        Playlist("Favorites", "Tus canciones preferidas")
    )

    private lateinit var adapter: PlaylistAdapter

    private lateinit var binding: ActivityPlaylistBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityPlaylistBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val recyclerView: RecyclerView = findViewById(R.id.recyclerViewPlaylists)
        adapter = PlaylistAdapter(playlists)

        recyclerView.layoutManager = LinearLayoutManager(this)
        recyclerView.adapter = adapter

        val linearLayout: LinearLayout = findViewById(R.id.playlistHeaderLayout)

        linearLayout.setOnClickListener {
            showAddPlaylistDialog()
        }

        initListener()
    }

    private fun initListener() {
        binding.Home.setOnClickListener {
            val intent = Intent(this@Playlist, CurrentActivity::class.java)
            startActivity(intent)
        }

        binding.ListCloud.setOnClickListener {
            val intent = Intent(this@Playlist, CloudMusicActivity::class.java)
            startActivity(intent)
        }

        binding.CrearCanco.setOnClickListener {
            val intent = Intent(this@Playlist, UploadSongActivity::class.java)
            startActivity(intent)
        }
    }

    private fun showAddPlaylistDialog() {
        val builder = AlertDialog.Builder(this)
        builder.setTitle("Add Playlist")

        val view = LayoutInflater.from(this).inflate(R.layout.dialog_add_playlist, null)
        builder.setView(view)

        val editTextName: EditText = view.findViewById(R.id.editTextName)
        val editTextArtist: EditText = view.findViewById(R.id.editTextArtist)

        builder.setPositiveButton("Agregar") { dialog, which ->
            val name = editTextName.text.toString()
            val artist = editTextArtist.text.toString()

            if (name.isNotEmpty() && artist.isNotEmpty()) {
                val newPlaylist = Playlist(name, artist)
                playlists.add(newPlaylist)
                adapter.notifyItemInserted(playlists.size - 1)
            } else {
                Toast.makeText(this, "Ingrese tanto nombre de artista como una descripcion", Toast.LENGTH_SHORT).show()
            }
        }

        builder.setNegativeButton("Cancelar") { dialog, which ->
            dialog.dismiss()
        }
        val dialog = builder.create()
        dialog.show()
    }
}