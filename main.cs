using System;
using System.Collections.Generic;

using SDG.Unturned;
using Steamworks;

using Rocket.API;
using Rocket.Core.Plugins;

using interception.discord;
using interception.discord.types;
using interception.serialization.types.discord;

namespace interception.plugins.discordstartannouncement {    
    public class config : IRocketPluginConfiguration, IDefaultable {
        public string webhook_url;
        public s_webhook webhook_settings;
       
        public void LoadDefaults() {
            webhook_url = "0_o";
            webhook_settings = new s_webhook() {
                username = "Server Start Announcer",
                avatar_url = "https://avatars.akamai.steamstatic.com/08c9944b3176faed9e762311495d14e2860a538a_full.jpg",
                content = "@everyone",
                embeds = new List<s_embed>() {
                    new s_embed() {
                        color = "#ff00ff",
                        author = new s_embed_author() {
                            name = "%SERVER_NAME%",
                            icon_url = "https://cdn2.steamgriddb.com/icon/775a46e8c6d09ce5548db66cc249435c/32/1024x1024.png"
                        },
                        title = "Server is up!",
                        fields = new List<s_embed_field>() {
                            new s_embed_field() {
                                name = "IP",
                                value = "%SERVER_IP%",
                                inline = true
                            },
                            new s_embed_field() {
                                name = "Port",
                                value = "%SERVER_PORT%",
                                inline = true
                            },
                            new s_embed_field() {
                                name = "Code",
                                value = "%SERVER_CODE%",
                                inline = true
                            },
                        },
                        image = new s_embed_image() {
                            url = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/304930/capsule_616x353.jpg"
                        }
                    }
                }
            };
        }
    }

    public class main : RocketPlugin<config> {
        internal static main instance;
        internal static config cfg;

        static void on_post_level_loaded(int level) {
            var wh = (webhook)cfg.webhook_settings;
            if (wh.embeds.Count > 0)
                wh.embeds[0].add_timestamp(DateTime.UtcNow);
            var data = wh.serialize_json_data()
                .Replace("%SERVER_NAME%", Provider.serverName)
                .Replace("%SERVER_IP%", SteamGameServer.GetPublicIP().ToIPAddress().MapToIPv4().ToString())
                .Replace("%SERVER_PORT%", Provider.port.ToString())
                .Replace("%SERVER_CODE%", SteamGameServer.GetSteamID().ToString());
            webhook_manager.send_webhook_async_unsafe(main.cfg.webhook_url, data, null);
        }

        protected override void Load() {
            instance = this;
            cfg = instance.Configuration.Instance;
            Level.onPostLevelLoaded += on_post_level_loaded;
            GC.Collect();
        }

        protected override void Unload() {
            Level.onPostLevelLoaded -= on_post_level_loaded;
            cfg = null;
            instance = null;
            GC.Collect();
        }
    }
}

