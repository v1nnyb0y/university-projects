const express = require('express')
const app = express()
const port = 2093
if (process.argv.length > 2) {
    port = process.argv[2] * 1;
  }

const albums_model = require('./models/albums')

app.use(express.json())
app.use(function (req, res, next) {
    res.setHeader('Access-Control-Allow-Origin', 'http://localhost:2093');
    res.setHeader('Access-Control-Allow-Methods', 'GET,POST');
    res.setHeader('Access-Control-Allow-Headers', 'Content-Type, Access-Control-Allow-Headers');
    next();
});

app.get('/get/myalbums', (req, res) => {
    albums_model.getAlbums()
        .then(response => {
            res.status(200).send(response);
        })
        .catch(error => {
            res.status(500).send(error);
        })
})

app.post('/post/myalbums', (req, res) => {
    albums_model.addAlbum(req.body)
        .then(response => {
            res.status(200).send(response);
        })
        .catch(error => {
            res.status(500).send(error);
        })
})

app.post('/post/upd_myalbum', (req, res) => {
    albums_model.updateAlbum(req.body)
        .then(response => {
            res.status(200).send(response);
        })
        .catch(error => {
            res.status(500).send(error);
        })
})

app.post('/post/del_myalbum', (req, res) => {
    albums_model.deleteAlbum(req.body) 
        .then(response => {
            res.status(200).send(response);
        })
        .catch(error => {
            res.status(500).send(error);
        })
});

if (port === 4000) {
    app.use('/api', app);
    app.listen(port, () => {
      console.log('Server started on: ' + port);
    });
  } else {
    module.exports = app;
}