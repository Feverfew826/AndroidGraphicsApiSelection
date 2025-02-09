package com.DefaultCompany.AndroidGraphicsApiSelection;
import com.unity3d.player.UnityPlayerActivity;
import android.os.Bundle;
import android.os.Build;

public class AndroidGraphicsApiSelectionActivity extends UnityPlayerActivity {

    private String appendCommandLineArgument(String cmdLine, String arg) {
        if (arg == null || arg.isEmpty())
            return cmdLine;
        else if (cmdLine == null || cmdLine.isEmpty())
            return arg;
        else
            return cmdLine + " " + arg; 
    } 

    @Override protected String updateUnityCommandLineArguments(String cmdLine)
    {
        String graphicsApiKey = "AndroidGraphicsApi";
        String savedGraphicsApi = PreferenceManager.getString(this, graphicsApiKey);
		
        if (savedGraphicsApi.equals("vulkan"))
            return appendCommandLineArgument(cmdLine, "-force-vulkan");
        else if (savedGraphicsApi.equals("gles"))
            return appendCommandLineArgument(cmdLine, "-force-gles");
        else
            return cmdLine; // let Unity pick the Graphics API based on PlayerSettings
    }

    @Override protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
    }
}