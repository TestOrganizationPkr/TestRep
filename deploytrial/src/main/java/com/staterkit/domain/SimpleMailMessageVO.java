package com.staterkit.domain;

import java.util.List;

/**
 * File : SimpleMailMessageVO.java 
 * Description : POJO Class which holds the input request object to send mail.
 *  Revision History :	Version	Date		 Author	Reason
 *   						0.1 01-Sep-2016	 Alex  Initial version
 */
public class SimpleMailMessageVO {
	private String from;
	private List<String> to;
	private List<String> cc;
	private List<String> bcc;

	private String subject;
	private String text;

	public SimpleMailMessageVO() {

	}

	public SimpleMailMessageVO(String from, List<String> to, List<String> cc, List<String> bcc, String subject,
			String text) {
		this.from = from;
		this.to = to;
		this.cc = cc;
		this.bcc = bcc;
		this.subject = subject;
		this.text = text;
	}

	public String getFrom() {
		return from;
	}

	public List<String> getTo() {
		return to;
	}

	public List<String> getCc() {
		return cc;
	}

	public List<String> getBcc() {
		return bcc;
	}

	public String getSubject() {
		return subject;
	}

	public String getText() {
		return text;
	}
}
