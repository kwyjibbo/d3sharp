﻿/*
 * Copyright (C) 2011 D3Sharp Project
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System.Collections.Generic;
using System.Linq;
using D3Sharp.Utils;
using D3Sharp.Net.BNet;
using D3Sharp.Core.Objects;

namespace D3Sharp.Core.Channels
{
    public static class ChannelManager
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public readonly static Dictionary<ulong, Channel> Channels =
            new Dictionary<ulong, Channel>();

        public static Channel CreateNewChannel(BNetClient client, ulong remoteObjectId)
        {
            var channel = new Channel(client, remoteObjectId);
            Channels.Add(channel.DynamicId, channel);
            return channel;
        }

        public static void DeleteChannel(ulong id)
        {
            if (!Channels.ContainsKey(id)) {
                Logger.Warn("Attempted to delete a non-existent channel with ID {0}", id);
                return;
            }
            var channel = Channels[id];
            channel.RemoveAllMembers();
            Channels.Remove(id);
        }
    }
}
