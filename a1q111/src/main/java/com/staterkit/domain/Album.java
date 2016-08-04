package com.staterkit.domain;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class Album
{
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    
    private String title;
    private String artist;
    private String releaseYear;

    public Album() {
    }

    /**
     * @param title
     * @param artist
     * @param releaseYear
     */
    public Album(String title, String artist, String releaseYear) {
        this.title = title;
        this.artist = artist;
        this.releaseYear = releaseYear;
    }

    /**
     * @return id
     */
    public Long getId() {
        return id;
    }

    /**
     * @param id
     */
    public void setId(Long id) {
        this.id = id;
    }

    /**
     * @return tittle
     */
    public String getTitle() {
        return title;
    }

    /**
     * @param title
     */
    public void setTitle(String title) {
        this.title = title;
    }

    /**
     * @return artist
     */
    public String getArtist() {
        return artist;
    }

    /**
     * @param artist
     */
    public void setArtist(String artist) {
        this.artist = artist;
    }

    /**
     * @return release year
     */
    public String getReleaseYear() {
        return releaseYear;
    }

    /**
     * @param releaseYear
     */
    public void setReleaseYear(String releaseYear) {
        this.releaseYear = releaseYear;
    }

    
    /* (non-Javadoc)
     * @see java.lang.Object#toString()
     */
    @Override
    public String toString() {
        return "Album{" +
                "id='" + id + '\'' +
                ", title='" + title + '\'' +
                ", artist='" + artist + '\'' +
                ", releaseYear='" + releaseYear + '\'' +
                '}';
    }
}
