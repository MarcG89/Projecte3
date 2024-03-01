package com.example.appmusicandroid.Adaper
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.bumptech.glide.Glide
import com.example.appmusicandroid.R
import com.example.appmusicandroid.Model.Album
import com.example.appmusicandroid.album_activity

class AlbumAdapter(private val albums: List<Album>) : RecyclerView.Adapter<AlbumAdapter.AlbumViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AlbumViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.current_playlist_row, parent, false)
        return AlbumViewHolder(view)
    }

    override fun onBindViewHolder(holder: AlbumViewHolder, position: Int) {
        val album = albums[position]

        holder.titleTextView.text = album.name
        holder.artistTextView.text = album.year.toString()

        Glide.with(holder.itemView)
            .load(album.frontCoverImage)
            .into(holder.albumImageView)

        holder.itemView.setOnClickListener {

            val intent = Intent(holder.itemView.context, album_activity::class.java)
            intent.putExtra("Name", album.name)
            intent.putExtra("Year", album.year)
            intent.putExtra("FrontCover", album.frontCoverID)
            intent.putExtra("BackCover", album.backCoverID)
            holder.itemView.context.startActivity(intent)
        }

    }

    inner class AlbumViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val titleTextView: TextView = itemView.findViewById(R.id.textName)
        val artistTextView: TextView = itemView.findViewById(R.id.artistName)
        val albumImageView: ImageView = itemView.findViewById(R.id.imageView)

    }

    override fun getItemCount() = albums.size

}