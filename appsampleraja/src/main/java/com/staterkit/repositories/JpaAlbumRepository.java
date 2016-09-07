package com.staterkit.repositories;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.staterkit.domain.Album;

/**
 * @author narendra.gurram@cognizant.com
 * @description JpaRepository reads system environment variables (VCAP_SERVICE) and establish a connection with DB using
 *   connection parameters. It creates a table 'Album' (domain class passed as an argument).
 *   Its having build-in methods for CRUD operation.
 *
 */
public interface JpaAlbumRepository extends MongoRepository<Album, String> {

}
