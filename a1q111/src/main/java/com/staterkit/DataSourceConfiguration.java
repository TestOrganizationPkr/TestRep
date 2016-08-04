package com.staterkit;

import org.springframework.boot.autoconfigure.jdbc.DataSourceProperties;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.cloud.Cloud;
import org.springframework.cloud.CloudFactory;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import javax.sql.DataSource;

/**
 * DB configuration class 
 * @author Anand.Kittappa@cognizant.com
 *
 */
@Configuration
public class DataSourceConfiguration
{

    /**
     * Creating cloud bean
     * @return cloud object
     */
    @Bean
    public Cloud cloud()
    {
        return new CloudFactory().getCloud();
    }

    /**
     * Creating datasource bean
     * @return DataSource object
     */
    @Bean
    @ConfigurationProperties(DataSourceProperties.PREFIX)
    public DataSource dataSource()
    {
        return cloud().getSingletonServiceConnector(DataSource.class, null);
    }

}
