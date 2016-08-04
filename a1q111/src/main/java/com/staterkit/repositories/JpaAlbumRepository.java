package com.staterkit.repositories;

import org.springframework.data.jpa.repository.JpaRepository;

import com.staterkit.domain.Album;

/**
 * JpaRepository reads system environment variables (VCAP_SERVICE) and establish
 * a connection with DB using connection parameters. It creates a table 'Album'
 * (domain class passed as an argument). It has built-in methods for CRUD
 * operation.
 * 
 * @author Anand.kittappa@cognizant.com.
 *
 */
public interface JpaAlbumRepository extends JpaRepository<Album, Long> {

}
