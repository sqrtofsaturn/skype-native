﻿using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using System;

namespace SkypeClient
{
    /// <summary>
    /// Abstractions for binding behaviour to the Lync SDK events.
    /// </summary>
    static class ExecuteAction
    {
        /// <summary>
        /// Execute an action on a conversation Modality when it is in a specific state.
        /// </summary>
        /// <param name="modality">the Modality to monitor</param>
        /// <param name="state">the state to execute in</param>
        /// <param name="action">an action delegate to run</param>
        public static void InState(Modality modality, ModalityState state, Action action)
        {
            if (modality.State == state) action();

            modality.ModalityStateChanged += (o, e) =>
            {
                if (e.NewState == state) action();
            };
        }

        /// <summary>
        /// Execute an action on all current conversations as well as any future ones.
        /// </summary>
        /// <param name="action">an Action delegate to run on each conversation</param>
        public static void OnAllConversations(Action<Conversation> action)
        {
            var client = LyncClient.GetClient();

            Util.ForEach(client.ConversationManager.Conversations, action);

            client.ConversationManager.ConversationAdded += (o, e) =>
            {
                action(e.Conversation);
            };
        }
    }
}