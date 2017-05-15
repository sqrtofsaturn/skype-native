﻿using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;
using Microsoft.Lync.Model.Conversation;

public class Startup
{
    public async Task<object> Invoke(dynamic options)
    {
        return Call(options.uri, options.fullscreen, options.display);
    }

    private static bool Call(string uri, bool fullscreen = true, int display = 0)
    {
        List<string> participants = new List<string>
        {
            uri
        };
  
        try
        {
            // TODO: switch this back to async
            ConversationWindow win = LyncClient.GetAutomation().EndStartConversation(
                LyncClient.GetAutomation().BeginStartConversation(
                    AutomationModalities.Video,
                    participants,
                    null,
                    null,
                    null
                )
            );

            if (fullscreen)
            {
                win.ShowFullScreen(display);
            }

            return true;
        }
        catch (ClientNotFoundException)
        {
            return false;
        }
    }
}