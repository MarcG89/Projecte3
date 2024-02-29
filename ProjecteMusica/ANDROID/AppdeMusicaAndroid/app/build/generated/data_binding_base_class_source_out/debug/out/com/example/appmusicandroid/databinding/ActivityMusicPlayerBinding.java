// Generated by view binder compiler. Do not edit!
package com.example.appmusicandroid.databinding;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.SeekBar;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.viewbinding.ViewBinding;
import androidx.viewbinding.ViewBindings;
import com.example.appmusicandroid.R;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import java.lang.NullPointerException;
import java.lang.Override;
import java.lang.String;

public final class ActivityMusicPlayerBinding implements ViewBinding {
  @NonNull
  private final RelativeLayout rootView;

  @NonNull
  public final ImageView IconLeft;

  @NonNull
  public final ImageView IconRight;

  @NonNull
  public final TextView InfoSubtitle;

  @NonNull
  public final LinearLayout PlayerHeader;

  @NonNull
  public final LinearLayout PlayerInfo;

  @NonNull
  public final TextView PlayerTitle;

  @NonNull
  public final LinearLayout Seekbar;

  @NonNull
  public final FloatingActionButton btnNext;

  @NonNull
  public final FloatingActionButton btnPlayOrPause;

  @NonNull
  public final FloatingActionButton btnPrev;

  @NonNull
  public final ImageView imageView2;

  @NonNull
  public final SeekBar seekBar;

  @NonNull
  public final TextView textDuration;

  @NonNull
  public final TextView textMusicName;

  @NonNull
  public final TextView textStart;

  private ActivityMusicPlayerBinding(@NonNull RelativeLayout rootView, @NonNull ImageView IconLeft,
      @NonNull ImageView IconRight, @NonNull TextView InfoSubtitle,
      @NonNull LinearLayout PlayerHeader, @NonNull LinearLayout PlayerInfo,
      @NonNull TextView PlayerTitle, @NonNull LinearLayout Seekbar,
      @NonNull FloatingActionButton btnNext, @NonNull FloatingActionButton btnPlayOrPause,
      @NonNull FloatingActionButton btnPrev, @NonNull ImageView imageView2,
      @NonNull SeekBar seekBar, @NonNull TextView textDuration, @NonNull TextView textMusicName,
      @NonNull TextView textStart) {
    this.rootView = rootView;
    this.IconLeft = IconLeft;
    this.IconRight = IconRight;
    this.InfoSubtitle = InfoSubtitle;
    this.PlayerHeader = PlayerHeader;
    this.PlayerInfo = PlayerInfo;
    this.PlayerTitle = PlayerTitle;
    this.Seekbar = Seekbar;
    this.btnNext = btnNext;
    this.btnPlayOrPause = btnPlayOrPause;
    this.btnPrev = btnPrev;
    this.imageView2 = imageView2;
    this.seekBar = seekBar;
    this.textDuration = textDuration;
    this.textMusicName = textMusicName;
    this.textStart = textStart;
  }

  @Override
  @NonNull
  public RelativeLayout getRoot() {
    return rootView;
  }

  @NonNull
  public static ActivityMusicPlayerBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, null, false);
  }

  @NonNull
  public static ActivityMusicPlayerBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup parent, boolean attachToParent) {
    View root = inflater.inflate(R.layout.activity_music_player, parent, false);
    if (attachToParent) {
      parent.addView(root);
    }
    return bind(root);
  }

  @NonNull
  public static ActivityMusicPlayerBinding bind(@NonNull View rootView) {
    // The body of this method is generated in a way you would not otherwise write.
    // This is done to optimize the compiled bytecode for size and performance.
    int id;
    missingId: {
      id = R.id.IconLeft;
      ImageView IconLeft = ViewBindings.findChildViewById(rootView, id);
      if (IconLeft == null) {
        break missingId;
      }

      id = R.id.IconRight;
      ImageView IconRight = ViewBindings.findChildViewById(rootView, id);
      if (IconRight == null) {
        break missingId;
      }

      id = R.id.InfoSubtitle;
      TextView InfoSubtitle = ViewBindings.findChildViewById(rootView, id);
      if (InfoSubtitle == null) {
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

      id = R.id.PlayerTitle;
      TextView PlayerTitle = ViewBindings.findChildViewById(rootView, id);
      if (PlayerTitle == null) {
        break missingId;
      }

      id = R.id.Seekbar;
      LinearLayout Seekbar = ViewBindings.findChildViewById(rootView, id);
      if (Seekbar == null) {
        break missingId;
      }

      id = R.id.btnNext;
      FloatingActionButton btnNext = ViewBindings.findChildViewById(rootView, id);
      if (btnNext == null) {
        break missingId;
      }

      id = R.id.btnPlayOrPause;
      FloatingActionButton btnPlayOrPause = ViewBindings.findChildViewById(rootView, id);
      if (btnPlayOrPause == null) {
        break missingId;
      }

      id = R.id.btnPrev;
      FloatingActionButton btnPrev = ViewBindings.findChildViewById(rootView, id);
      if (btnPrev == null) {
        break missingId;
      }

      id = R.id.imageView2;
      ImageView imageView2 = ViewBindings.findChildViewById(rootView, id);
      if (imageView2 == null) {
        break missingId;
      }

      id = R.id.seekBar;
      SeekBar seekBar = ViewBindings.findChildViewById(rootView, id);
      if (seekBar == null) {
        break missingId;
      }

      id = R.id.textDuration;
      TextView textDuration = ViewBindings.findChildViewById(rootView, id);
      if (textDuration == null) {
        break missingId;
      }

      id = R.id.textMusicName;
      TextView textMusicName = ViewBindings.findChildViewById(rootView, id);
      if (textMusicName == null) {
        break missingId;
      }

      id = R.id.textStart;
      TextView textStart = ViewBindings.findChildViewById(rootView, id);
      if (textStart == null) {
        break missingId;
      }

      return new ActivityMusicPlayerBinding((RelativeLayout) rootView, IconLeft, IconRight,
          InfoSubtitle, PlayerHeader, PlayerInfo, PlayerTitle, Seekbar, btnNext, btnPlayOrPause,
          btnPrev, imageView2, seekBar, textDuration, textMusicName, textStart);
    }
    String missingId = rootView.getResources().getResourceName(id);
    throw new NullPointerException("Missing required view with ID: ".concat(missingId));
  }
}
