package com.staterkit.controller;
/**
 * 
 */


import org.junit.Before;
import org.junit.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.boot.test.SpringApplicationConfiguration;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;

import com.staterkit.Application;
import com.staterkit.controllers.AlbumController;
import com.staterkit.repositories.JpaAlbumRepository;
import com.staterkit.service.AlbumService;

/**
 * @author narendra.gurram@cognizant.com
 *
 */
@SpringApplicationConfiguration(classes = Application.class)   
public class ControllerTest {

		@Mock
		private JpaAlbumRepository jpaAlbumRepository;
		
		@InjectMocks
		private AlbumService albumsService;

		private MockMvc mockMvc;
		
		
		@Before
		public void setUp() {
			MockitoAnnotations.initMocks(this);
			this.mockMvc = MockMvcBuilders.standaloneSetup(new AlbumController()).build();
		}
		 @Test
		 public void getAlbumsTest	() throws Exception{
			 this.mockMvc.perform(MockMvcRequestBuilders.get("/albums"));
			
		 }
		 
		 @Test
		 public void deleteAlbumsTest	() throws Exception{
			 this.mockMvc.perform(MockMvcRequestBuilders.get("/deletealbum?id=1"));
			
		 }
		 
		 @Test
		 public void addAlbumTest	() throws Exception{
			 this.mockMvc.perform(MockMvcRequestBuilders.post("/addAlbum?title=test&artist=test&releaseYear=2016"));
			
		 }
		
		 @Test
		 public void editAlbumTest	() throws Exception{
			 this.mockMvc.perform(MockMvcRequestBuilders.post("/editAlbum?id=1&title=test&artist=test&releaseYear=2016"));
			
		 }
		
		 
	}
