const Pool = require('pg').Pool
const connectionString = 'postgres://pinjizzl:bwR-s9letBp3hssMs_IsH3KTwq-PoQcQ@dumbo.db.elephantsql.com/pinjizzl'

const pool = new Pool({
    connectionString
});

const getAlbums = () => {
    return new Promise(function(resolve, reject) {
        pool.query('SELECT * FROM "public"."albumtest" WHERE current_date - created_date >= 1 ORDER BY created_date', (error, results) => {
            if (error) {
                reject(error)
            }
            resolve(results.rows);
        })
    }) 
};

const addAlbum = (body) => {
    return new Promise(function(resolve, reject) {
        const { id } = body
        pool.query('INSERT INTO "public"."albumtest" (id, created_date) VALUES ($1, current_date) RETURNING *', [ id ] , (error, results) => {
            if (error) {
                reject(error)
            }
            resolve(true);
        })
    })
}

const updateAlbum = (body) => {
    return new Promise(function(resolve, reject) {
        const { id } = body
        pool.query('UPDATE albumtest SET created_date=current_date WHERE id=$1', [id], (error, results) => {
            if (error) {
                reject(error);
            }
            resolve(true);
        })
    })
}

const deleteAlbum = (body) => {
    return new Promise(function(resolve, reject) {
        const { id } = body;
        pool.query('DELETE FROM albumtest WHERE id = $1', [ id ], (error, results) => {
            if (error) {
                reject(error);
            }
            resolve(true);
        })
    })
}

module.exports = {
    getAlbums,
    addAlbum,
    updateAlbum,
    deleteAlbum
}
