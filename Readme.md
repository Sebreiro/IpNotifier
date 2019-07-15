IP Address Change Notifier
===
.NET Core application that gets current public IP Address and sends it to the API Endpoint

### **Note**

I have a dynamic public IP Address, so it's usefull to know the current one

https://ipify.org is used to get current public IP Address

Application was developed to send messages to the [TelegramBotMessageSender](https://github.com/Sebreiro/TelegramBotMessageSender)

## **Config**

_appsettings.json_  - main config and it's requeired 


### **appsettings.json**

File structure:  
```JSON
{
  "jobConfig": {
    "requestInterval": 60
  },
  "messageSender": {
    "channelName": "<FictionalChannelNameForMappingToTheTelegramChannelId>",
    "url": "http://<host>:<port>/api/Message/SendMessageAsync"
  },
  "Serilog": {
    "Using": [
      "Serilog.Sinks.Console",
      "Serilog.Sinks.Graylog"
    ],
    "MinimumLevel": {
      "Default": "Verbose"
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "restrictedToMinimumLevel": "Information"
        }
      },
      {
        "Name": "Graylog",
        "Args": {
          "hostnameOrAddress": "hostname",
          "port": "12201",
          "transportType": "Udp",
          "facility": "IPNotifier"
        }
      }
    ]
  }
}
```

`jobConfig.requestInterval` - time in **minutes** till the next IP Address reqeust. To change this parameter application has to be restarted.

`messageSender.url` - url where message will be send  
`messageSender.channelName` - fictional channel name that maps to the telegram channelId in [TelegramBotMessageSender](https://github.com/Sebreiro/TelegramBotMessageSender)

`Serilog` - [Serilog logging configuration](https://github.com/serilog/serilog-settings-configuration) with [Graylog sink](https://github.com/whir1/serilog-sinks-graylog)
