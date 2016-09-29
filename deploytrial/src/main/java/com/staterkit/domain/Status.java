package com.staterkit.domain;

/**
 * File : Status.java 
 * Description : POJO Class which holds the send mail reponse.
 *  Revision History :	Version	Date		 Author	Reason
 *   						0.1 01-Sep-2016	 Alex  Initial version
 */
public class Status {

	private String message;
	private String status;

	public String getStatus() {
		return status;
	}

	public Status() {		
		this.message = "";
	}

	public Status(String status, String message) {
		this.status = status;
		this.message = message;
	}

	public String getMessage() {
		return message;
	}
}