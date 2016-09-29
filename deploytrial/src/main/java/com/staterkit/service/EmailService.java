package com.staterkit.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mail.MailSender;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.stereotype.Service;

import com.staterkit.domain.Status;

/**
 * File : EmailService.java 
 * Description : Service class to send mail based on the input request using sendgrid.
 *  Revision History :	Version	Date		 Author	Reason
 *   						0.1 01-Sep-2016	 Alex  Initial version
 */

@Service	
public class EmailService {
	@Autowired
	private MailSender mailSender;

	/**
	 * This service method to send mail using sendgrid service 
	 * @msg: will have all the inputs to send mail 
	 */	
	public Status send(SimpleMailMessage msg) {
		try {			
			mailSender.send(msg);
		} catch (Exception e) {		
			return new Status("failure", e.getMessage());
		}
		return new Status("success","Mail Sent Successfully");
	}
}
