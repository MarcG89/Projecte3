// Generated by view binder compiler. Do not edit!
package com.example.appmusicandroid.databinding;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.recyclerview.widget.RecyclerView;
import androidx.viewbinding.ViewBinding;
import androidx.viewbinding.ViewBindings;
import com.example.appmusicandroid.R;
import java.lang.NullPointerException;
import java.lang.Override;
import java.lang.String;

public final class ActivityAlbumBinding implements ViewBinding {
  @NonNull
  private final RelativeLayout rootView;

  @NonNull
  public final TextView albumName;

  @NonNull
  public final ImageView backCover;

  @NonNull
  public final ImageView frontCover;

  @NonNull
  public final RecyclerView songList;

  @NonNull
  public final TextView year;

  private ActivityAlbumBinding(@NonNull RelativeLayout rootView, @NonNull TextView albumName,
      @NonNull ImageView backCover, @NonNull ImageView frontCover, @NonNull RecyclerView songList,
      @NonNull TextView year) {
    this.rootView = rootView;
    this.albumName = albumName;
    this.backCover = backCover;
    this.frontCover = frontCover;
    this.songList = songList;
    this.year = year;
  }

  @Override
  @NonNull
  public RelativeLayout getRoot() {
    return rootView;
  }

  @NonNull
  public static ActivityAlbumBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, null, false);
  }

  @NonNull
  public static ActivityAlbumBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup parent, boolean attachToParent) {
    View root = inflater.inflate(R.layout.activity_album, parent, false);
    if (attachToParent) {
      parent.addView(root);
    }
    return bind(root);
  }

  @NonNull
  public static ActivityAlbumBinding bind(@NonNull View rootView) {
    // The body of this method is generated in a way you would not otherwise write.
    // This is done to optimize the compiled bytecode for size and performance.
    int id;
    missingId: {
      id = R.id.albumName;
      TextView albumName = ViewBindings.findChildViewById(rootView, id);
      if (albumName == null) {
        break missingId;
      }

      id = R.id.backCover;
      ImageView backCover = ViewBindings.findChildViewById(rootView, id);
      if (backCover == null) {
        break missingId;
      }

      id = R.id.frontCover;
      ImageView frontCover = ViewBindings.findChildViewById(rootView, id);
      if (frontCover == null) {
        break missingId;
      }

      id = R.id.songList;
      RecyclerView songList = ViewBindings.findChildViewById(rootView, id);
      if (songList == null) {
        break missingId;
      }

      id = R.id.year;
      TextView year = ViewBindings.findChildViewById(rootView, id);
      if (year == null) {
        break missingId;
      }

      return new ActivityAlbumBinding((RelativeLayout) rootView, albumName, backCover, frontCover,
          songList, year);
    }
    String missingId = rootView.getResources().getResourceName(id);
    throw new NullPointerException("Missing required view with ID: ".concat(missingId));
  }
}
