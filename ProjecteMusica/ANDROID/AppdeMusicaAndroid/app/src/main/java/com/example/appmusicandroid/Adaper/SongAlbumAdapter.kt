package com.example.appmusicandroid.Adaper

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.example.appmusicandroid.Model.SongSql
import com.example.appmusicandroid.R

class SongAlbumAdapter(private val songs: List<SongSql>) : RecyclerView.Adapter<SongAlbumAdapter.SongViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): SongViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.current_recycler_row, parent, false)
        return SongViewHolder(view)
    }

    override fun getItemCount(): Int {
    return songs.size
    }

    override fun onBindViewHolder(holder: SongViewHolder, position: Int) {
        val song = songs[position]
        holder.titleTextView.text = song.title
    }

    inner class SongViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val titleTextView: TextView = itemView.findViewById(R.id.textName)
    }
}