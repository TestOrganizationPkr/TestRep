package com.staterkit.utils;

import java.util.Map.Entry;
import java.util.Set;

import org.springframework.stereotype.Component;

import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

@Component
public class VCAPUtility {
	public JsonObject getCredentials(String name) {
		try {
			String vcap_string = System.getenv("VCAP_SERVICES");
			JsonObject vcap_services = (JsonObject) new JsonParser().parse(vcap_string);
			Set<Entry<String, JsonElement>> entries = vcap_services.entrySet();
			for (Entry<String, JsonElement> eachEntry : entries) {
				if (eachEntry.getKey().toLowerCase().contains(name)) {
					vcap_services = (JsonObject) ((JsonArray) eachEntry.getValue()).get(0);
					JsonObject credentialsObj = (JsonObject) vcap_services.get("credentials");
					return credentialsObj;
				}
			}
			JsonArray userProvided = (JsonArray) vcap_services.get("user-provided");
			if (userProvided != null) {
				for (JsonElement cupsElement : userProvided) {
					JsonObject cups = cupsElement.getAsJsonObject();
					String cupsName = cups.get("name").getAsString();
					if (cupsName.toLowerCase().contains(name)) {
						return cups.get("credentials").getAsJsonObject();
					}
				}
			}
		} catch (Exception e) {
			return null;
		}
		return null;
	}
}
