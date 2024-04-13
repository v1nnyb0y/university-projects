import React from 'react';
import { Card } from 'react-bootstrap';
import _ from 'lodash';
import music from '../images/music.jpeg';

const TracksList = (props) => {
    const { tracks, onAddTrackToPlaylist, myalbums } = props;

    const handleAddTrackToPlaylist = (uri) => {
        if (myalbums && myalbums.items && myalbums.items.length > 0) {
            onAddTrackToPlaylist(uri);
        }
    }

  return (
    <div>
      {Object.keys(tracks).length > 0 && (
        <div className="tracks">
          {tracks.items.map((item, index) => {
            return (
              <React.Fragment key={index}>
                <Card style={{ width: '18rem' }}>
                    <div className="card-image-link" onClick={() => handleAddTrackToPlaylist(item.uri)}>
                        {!_.isEmpty(item.album.images) ? (
                        <Card.Img variant="top" src={item.album.images[0].url} alt="" />
                        ) : (
                        <img src={music} alt="" />
                        )}
                    </div>
                  <Card.Body>
                    <Card.Title>{item.name}</Card.Title>
                    <Card.Text>
                      <small>By {item.artists.map((val, id) => {
                          return val.name + '; '
                      })}</small>
                    </Card.Text>
                  </Card.Body>
                </Card>
              </React.Fragment>
            );
          })}
        </div>
      )}
    </div>
  );
};

export default TracksList;
