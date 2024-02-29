// Generated by view binder compiler. Do not edit!
package com.example.appmusicandroid.databinding;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.viewbinding.ViewBinding;
import androidx.viewbinding.ViewBindings;
import com.example.appmusicandroid.R;
import java.lang.NullPointerException;
import java.lang.Override;
import java.lang.String;

public final class ActivityPlayCloudMusicBinding implements ViewBinding {
  @NonNull
  private final RelativeLayout rootView;

  @NonNull
  public final ImageView IconLeft;

  @NonNull
  public final LinearLayout PlayerHeader;

  @NonNull
  public final LinearLayout PlayerInfo;

  @NonNull
  public final Button btnDownload;

  @NonNull
  public final TextView durationSong;

  @NonNull
  public final TextView languageSong;

  @NonNull
  public final TextView versionOriginalSong;

  private ActivityPlayCloudMusicBinding(@NonNull RelativeLayout rootView,
      @NonNull ImageView IconLeft, @NonNull LinearLayout PlayerHeader,
      @NonNull LinearLayout PlayerInfo, @NonNull Button btnDownload, @NonNull TextView durationSong,
      @NonNull TextView languageSong, @NonNull TextView versionOriginalSong) {
    this.rootView = rootView;
    this.IconLeft = IconLeft;
    this.PlayerHeader = PlayerHeader;
    this.PlayerInfo = PlayerInfo;
    this.btnDownload = btnDownload;
    this.durationSong = durationSong;
    this.languageSong = languageSong;
    this.versionOriginalSong = versionOriginalSong;
  }

  @Override
  @NonNull
  public RelativeLayout getRoot() {
    return rootView;
  }

  @NonNull
  public static ActivityPlayCloudMusicBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, null, false);
  }

  @NonNull
  public static ActivityPlayCloudMusicBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup parent, boolean attachToParent) {
    View root = inflater.inflate(R.layout.activity_play_cloud_music, parent, false);
    if (attachToParent) {
      parent.addView(root);
    }
    return bind(root);
  }

  @NonNull
  public static ActivityPlayCloudMusicBinding bind(@NonNull View rootView) {
    // The body of this method is generated in a way you would not otherwise write.
    // This is done to optimize the compiled bytecode for size and performance.
    int id;
    missingId: {
      id = R.id.IconLeft;
      ImageView IconLeft = ViewBindings.findChildViewById(rootView, id);
      if (IconLeft == null) {
        break missingId;
      }

      id = R.id.PlayerHeader;
      LinearLayout PlayerHeader = ViewBindings.findChildViewById(rootView, id);
      if (PlayerHeader == null) {
        break missingId;
      }

      id = R.id.PlayerInfo;
      LinearLayout PlayerInfo = ViewBindings.findChildViewById(rootView, id);
      if (PlayerInfo == null) {
        break missingId;
      }

      id = R.id.btnDownload;
      Button btnDownload = ViewBindings.findChildViewById(rootView, id);
      if (btnDownload == null) {
        break missingId;
      }

      id = R.id.durationSong;
      TextView durationSong = ViewBindings.findChildViewById(rootView, id);
      if (durationSong == null) {
        break missingId;
      }

      id = R.id.languageSong;
      TextView languageSong = ViewBindings.findChildViewById(rootView, id);
      if (languageSong == null) {
        break missingId;
      }

      id = R.id.versionOriginalSong;
      TextView versionOriginalSong = ViewBindings.findChildViewById(rootView, id);
      if (versionOriginalSong == null) {
        break missingId;
      }

      return new ActivityPlayCloudMusicBinding((RelativeLayout) rootView, IconLeft, PlayerHeader,
          PlayerInfo, btnDownload, durationSong, languageSong, versionOriginalSong);
    }
    String missingId = rootView.getResources().getResourceName(id);
    throw new NullPointerException("Missing required view with ID: ".concat(missingId));
  }
}
