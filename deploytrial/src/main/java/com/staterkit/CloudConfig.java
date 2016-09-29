package com.staterkit;

import org.springframework.cloud.config.java.AbstractCloudConfig;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.mongodb.MongoDbFactory;


/**
 * @author narendra.gurram@cognizant.com 
 * This CloudConfig class get the connection from mongoDbFactory 
 */
@Configuration
public class CloudConfig extends AbstractCloudConfig {
 @Bean
 public MongoDbFactory documentMongoDbFactory() {
     return connectionFactory().mongoDbFactory();
 }
}

