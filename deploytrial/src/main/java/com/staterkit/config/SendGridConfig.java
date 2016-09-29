package com.staterkit.config;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.mail.MailSender;
import org.springframework.mail.javamail.JavaMailSenderImpl;

import com.google.gson.JsonObject;
import com.staterkit.utils.VCAPUtility;

/**
 * /*** File : SendGridConfig.java 
 * Description : This Class is to make Send grid SMTP configuration for cloud environment. The same class can also be used 
 *  for local environment by providing the connection details directly
 *  Revision History :	Version	Date		 Author	Reason
 *   						0.1 01-Sep-2016	 Alex  Initial version
 */
@Configuration
public class SendGridConfig {
	
	@Autowired
	private VCAPUtility vCAPUtility;
	
	@Value("${sendgrid.hostname}")
	private String hostName;

	@Value("${sendgrid.password}")
	private String password;

	@Value("${sendgrid.username}")
	private String userName;
	
	/**
	 * This method will read the connection details from VCAP_PROPERTIES and will do the SMTP connection
	 * 
	 * @return
	 */
	@Bean
	public MailSender mailSender() {
		
		JsonObject cloudConfig = vCAPUtility.getCredentials("sendgrid");
		JavaMailSenderImpl mailSenderImpl = new JavaMailSenderImpl();
		if (cloudConfig!=null){			
			mailSenderImpl.setHost(cloudConfig.get("hostname").getAsString());
			mailSenderImpl.setUsername(cloudConfig.get("username").getAsString());
			mailSenderImpl.setPassword(cloudConfig.get("password").getAsString());
			
		}else{			
			mailSenderImpl.setHost(hostName);
			mailSenderImpl.setUsername(userName);
			mailSenderImpl.setPassword(password);
		}
		return mailSenderImpl;
	}
}