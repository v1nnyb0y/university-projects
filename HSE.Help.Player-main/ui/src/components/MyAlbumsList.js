import React from 'react';
import { Card } from 'react-bootstrap';
import _ from 'lodash';
import music from '../images/music.jpeg';

const MyAlbumsList = ({ myalbums, onDeleteAlbum }) => {
    const handleDeleteAlbum = (albumId) => {
        onDeleteAlbum(albumId);
    }

  return (
    <React.Fragment>
      {Object.keys(myalbums.items).length > 0 && (
        <div className="myAlbums">
          {myalbums.items.map((album, index) => {
            return (
              <React.Fragment key={index}>
                <Card style={{ width: '18rem' }}>
                    <div className='delete' onClick={() => handleDeleteAlbum(album.id)}></div>
                  <a
                    target="_blank"
                    href={album.url}
                    rel="noopener noreferrer"
                    className="card-image-link"
                  >
                    {!_.isEmpty(album.images) ? (
                      <Card.Img
                        variant="top"
                        src={album.images[0].url}
                        alt=""
                      />
                    ) : (
                      <img src={music} alt="" />
                    )}
                  </a>
                  <Card.Body>
                    <Card.Title>{album.name}</Card.Title>
                    <Card.Text>
                      <small>
                        {album.displayName}
                      </small>
                    </Card.Text>
                  </Card.Body>
                </Card>
              </React.Fragment>
            );
          })}
        </div>
      )}
    </React.Fragment>
  );
};

export default MyAlbumsList;
