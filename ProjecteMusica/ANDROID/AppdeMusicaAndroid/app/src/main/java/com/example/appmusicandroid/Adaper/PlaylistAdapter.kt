package com.example.appmusicandroid.Adaper

import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.appmusicandroid.R
import com.example.appmusicandroid.View.Playlist
import com.example.appmusicandroid.View.PlaylistSong

class PlaylistAdapter(private val playlists: List<Playlist.Playlist>) : RecyclerView.Adapter<PlaylistAdapter.PlaylistViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PlaylistViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.current_playlist_row, parent, false)
        return PlaylistViewHolder(view)
    }

    override fun onBindViewHolder(holder: PlaylistViewHolder, position: Int) {
        val playlist = playlists[position]

        holder.textName.text = playlist.name
        holder.artistName.text = playlist.artist

        holder.itemView.setOnClickListener {
            val intent = Intent(holder.itemView.context, PlaylistSong::class.java)
            intent.putExtra("playlistName", playlist.name)
            holder.itemView.context.startActivity(intent)
        }
    }

    override fun getItemCount() = playlists.size
    inner class PlaylistViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val textName: TextView = itemView.findViewById(R.id.textName)
        val artistName: TextView = itemView.findViewById(R.id.artistName)
    }
}