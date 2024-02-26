import config from "@colyseus/tools";
import { monitor } from "@colyseus/monitor";
import { playground } from "@colyseus/playground";
import { auth } from "@colyseus/auth";
import path from 'path';
import serveIndex from 'serve-index';
import express from 'express';

// import { uWebSocketsTransport} from "@colyseus/uwebsockets-transport";
import "./config/auth";

// Import demo room handlers
import { LobbyRoom, RelayRoom } from 'colyseus';
import { StateHandlerRoom } from "./rooms/GameRoom";
import { CustomLobbyRoom } from './rooms/LobbyRoom';

export default config({
    options: {
        devMode: true,
    },

    initializeGameServer: (gameServer) => {
        gameServer.define("lobby", LobbyRoom);

        gameServer.define("relay", RelayRoom, { maxClients: 4 })
            .enableRealtimeListing();

        gameServer.define("game", StateHandlerRoom)
            .enableRealtimeListing();

        gameServer.define("customLobby", CustomLobbyRoom);

        gameServer.onShutdown(function(){
            console.log(`game server is going down.`);
        });
    },

    initializeExpress: (app) => {
        app.use(auth.prefix, auth.routes());

        app.use('/playground', playground);

        app.use('/colyseus', monitor());

        app.use('/', serveIndex(path.join(__dirname, "static"), {'icons': true}))
        app.use('/', express.static(path.join(__dirname, "static")));
    },


    beforeListen: () => {
        /**
         * Before before gameServer.listen() is called.
         */
    }
});
