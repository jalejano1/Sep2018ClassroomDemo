﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {

                //code to go here
                var results = from x in context.PlaylistTracks
                              where x.Playlist.UserName.Equals(username)
                                  && x.Playlist.Name.Equals(playlistname)
                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  TrackName = x.Track.Name,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };

                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            //the using sets up the transactions environment


            //a List<string> to be used to handle any number of errors 
            //generated while doing the transaction 
            List<string> reasons = new List<string>();
            using (var context = new ChinookContext())
            {
                
                //code to go here
                //Part two
                Playlist exists = context.Playlists.Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) 
                                            && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase)).Select(x => x).FirstOrDefault();
                //create an instance for PlaylistTrack
                PlaylistTrack newTrack = null;
                // initialize a local tracknumber 
                int tracknumber = 0;
                if(exists == null)
                {
                    //this is a new playlist being created 
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists);
                    tracknumber = 1;
                }
                else
                {
                    //this is an existing playlist
                    //calculate the new proposed tracknumber
                    tracknumber = exists.PlaylistTracks.Count() + 1;
                    //business rule: track may only exists once on a playlist it may exists on many different playlists
                    //.SingleOrDefault() expects a single instance to be returned
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    if(newTrack != null)
                    {
                        reasons.Add("Track already exists on the playlist.");
                    }
                }
                if(reasons.Count() > 0)
                {
                    //issue the businessRuleExeption(title, list of error strings)
                    throw new BusinessRuleException("Adding track to playlist", reasons);
                }
                else
                {
                    //Part two
                    newTrack = new PlaylistTrack();
                    newTrack.TrackId = trackid;
                    newTrack.TrackNumber = tracknumber;

                    //whatabout the PlaylistId?
                    //Note: the pkey for PlaylistId may not yet exists
                    //     using navigation one can let HashSet handle the expected
                    //      Playlist pkey value
                    exists.PlaylistTracks.Add(newTrack);
                    //at this point all records are in staged state
                    //physically add all data for the transaction to the database and commit
                    context.SaveChanges();
                }
             
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
