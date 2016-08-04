/**
 * 
 */
package com.staterkit;

import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

/**
 * This is a Spring boot application class which implements a CommandLineRunner.
 * This class will create and load all the required beans during runtime
 * 
 * @author Anand.Kittappa@cognizant.com.
 *
 */

@SpringBootApplication
public class Application implements CommandLineRunner {

	/**
	 * Loading all the required beans for this demo application
	 * 
	 * @param args
	 */
	public static void main(String[] args) throws Exception {
		SpringApplication.run(new Object[] { Application.class }, args);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see org.springframework.boot.CommandLineRunner#run(java.lang.String[])
	 */
	@Override
	public void run(String... args) throws Exception {

	}
}
