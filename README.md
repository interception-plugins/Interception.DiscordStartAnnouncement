# Interception.DiscordStartAnnouncement

Unturned RocketMod plugin that sends an embed message to Discord text channel via webhook when server starts
	
A part of [Interception.Module](https://github.com/interception-plugins/Interception.Module) example plugins

## Configuration

```xml
<?xml version="1.0" encoding="utf-8"?>
<config xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <webhook_url>0_o</webhook_url>
  <webhook_settings username="Server Start Announcer" avatar_url="https://avatars.akamai.steamstatic.com/08c9944b3176faed9e762311495d14e2860a538a_full.jpg">
    <content>@everyone</content>
    <embeds>
      <embed color="#ff00ff" title="Server is up!">
        <author name="%SERVER_NAME%" icon_url="https://cdn2.steamgriddb.com/icon/775a46e8c6d09ce5548db66cc249435c/32/1024x1024.png" />
        <fields>
          <field name="IP" inline="true">
            <value>%SERVER_IP%</value>
          </field>
          <field name="Port" inline="true">
            <value>%SERVER_PORT%</value>
          </field>
          <field name="Code" inline="true">
            <value>%SERVER_CODE%</value>
          </field>
        </fields>
        <image url="https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/304930/capsule_616x353.jpg" />
        <timestamp xsi:nil="true" />
      </embed>
    </embeds>
    <flags />
    <files />
  </webhook_settings>
</config>
```