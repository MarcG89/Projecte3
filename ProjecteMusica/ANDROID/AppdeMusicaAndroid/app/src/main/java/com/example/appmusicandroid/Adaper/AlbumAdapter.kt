package com.example.appmusicandroid.Adaper
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.example.appmusicandroid.R
import com.example.appmusicandroid.Model.Album
class AlbumAdapter(private val albums: List<Album>) : RecyclerView.Adapter<AlbumAdapter.AlbumViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AlbumViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.current_playlist_row, parent, false)
        return AlbumViewHolder(view)
    }

    override fun onBindViewHolder(holder: AlbumViewHolder, position: Int) {
        val album = albums[position]

        holder.titleTextView.text = album.Name
        holder.artistTextView.text = album.Artist

        Glide.with(holder.itemView)
            .load(album.FrontCover)
            .into(holder.albumImageView)

    }

    inner class AlbumViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val titleTextView: TextView = itemView.findViewById(R.id.textName)
        val artistTextView: TextView = itemView.findViewById(R.id.artistName)
        val albumImageView: ImageView = itemView.findViewById(R.id.imageView)
    }


    override fun getItemCount() = albums.size

}