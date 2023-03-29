const express = require('express');
const fs = require('fs');
const path = require('path');
const bodyParser = require('body-parser');

const host = process.env.HOST || '127.0.0.1';
const port = process.env.PORT || 5000;

const app = express();
app.use(bodyParser.json());
app.use('/TemplateData',express.static(path.join('TemplateData')))

app.get('/', async function (req, res, next) {
    getRest();
    res.writeHead(200, { "content-Type": "texthtml" });
    console.log("requested index.html");
    fs.createReadStream("./index.html").pipe(res)
    
});

function getRest() {
    app.get("/Build/UnityLoader.js", function (req, res) {
        res.writeHead(200, { "content-Type": "text/javaScript" });
        console.log("requested UnityLoader.js");
        fs.createReadStream("./Build/UnityLoader.js").pipe(res);
        
    })
    app.get("/Build/fafeGame.data.unityweb", function (req, res) {
        res.writeHead(200, { "content-Type": "text/data/unityweb" });
        console.log("requested fafeGame.data.unitywebs");
        fs.createReadStream("./Build/fafeGame.data.unityweb").pipe(res);
        
    })
    app.get("/Build/fafeGame.json", function (req, res) {
        res.writeHead(200, { "content-Type": "text/json" });
        console.log("requested fafeGame.json");
        fs.createReadStream("./Build/fafeGame.json").pipe(res);
        
    })
    app.get("/Build/fafeGame.wasm.code.unityweb", function (req, res) {
        res.writeHead(200, { "content-Type": "text/wasm/code/unityweb" });
        console.log("requested fafeGame.wasm.code.unityweb");
        fs.createReadStream("./Build/fafeGame.wasm.code.unityweb").pipe(res);
        
    })
    app.get("/Build/fafeGame.wasm.framework.unityweb", function (req, res) {
        res.writeHead(200, { "content-Type": "text/wasm/framework/unityweb" });
        console.log("requested fafeGame.wasm.framework.unityweb");
        fs.createReadStream("./Build/fafeGame.wasm.framework.unityweb").pipe(res);
        
    })
    
    app.get("/TemplateData/UnityProgress.js", function (req, res) {
        res.writeHead(200, { "content-Type": "text/javaScript" });
        console.log("requested UnityProgress.js");
        fs.createReadStream("./TemplateData/UnityProgress.js").pipe(res);
        
    })
    
   /* app.get("/TemplateData/fullscreen.png", function (req, res) {
        res.writeHead(200, { "content-Type": "text/png" });
        console.log("requested fullscreen.png");
        fs.createReadStream("./TemplateData/fullscreen.png").pipe(res);
    })*/

    app.get("/TemplateData/progressEmpty.Dark.png", function (req, res) {
        console.log("ok")
        res.writeHead(200, { "content-Type": "text/Dark/png" });
        console.log("requested progressEmpty.Dark.png");
        fs.createReadStream("./TemplateData/progressEmpty.Dark.png").pipe(res);
    })



}

//Server Listening
app.listen(port, function (err) {
    if (!err) {
        console.log('Server hosting on ' + host + ' and PORT ' + port);
    }
    else {
        console.log(err);
    }
})