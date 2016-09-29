package com.staterkit.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.google.gson.Gson;
import com.staterkit.domain.SimpleMailMessageVO;
import com.staterkit.domain.Status;
import com.staterkit.service.EmailService;

/**
 * File : SendGridController.java 
 * Description : This Class is to create a rest api to send mail using SendGrid service.
 * the request will be in the JSON Format which holds all the mail details
 *  Revision History :	Version	Date		 Author	Reason
 *   						0.1 01-Sep-2016	 Alex  Initial version
 */
@RestController
@CrossOrigin
public class SendGridController {

	@Autowired
	private EmailService emailService;
	
	/**
	 * This rest api method to send mail using sendgrid service 
	 * @request: will have all the inputs to send mail 
	 */	
	@RequestMapping(value = "/sendmail", method = RequestMethod.POST )
	@CrossOrigin
	public Status sendMail(@RequestBody String request) {
		System.out.println("send mail called");
		SimpleMailMessage emailMessage = new SimpleMailMessage();
		Gson gson = new Gson();
		SimpleMailMessageVO emailMessageVO = gson.fromJson(request, SimpleMailMessageVO.class);

		emailMessage.setFrom(emailMessageVO.getFrom());
		emailMessage.setTo(listToArray(emailMessageVO.getTo()));
		emailMessage.setCc(listToArray(emailMessageVO.getCc()));
		emailMessage.setBcc(listToArray(emailMessageVO.getBcc()));

		emailMessage.setSubject(emailMessageVO.getSubject());
		emailMessage.setText(emailMessageVO.getText());
		return emailService.send(emailMessage);

	}
	
	/**
	 * This method will convert the list object into array
	 * @list: input list details holds the CC , BCC and TO mail id list 
	 */
	private String[] listToArray(List<String> list) {
		if (list!=null && list.size()>0){
			return list.toArray(new String[list.size()]);
		}else{
			return null;
		}
	}
	
	
}
