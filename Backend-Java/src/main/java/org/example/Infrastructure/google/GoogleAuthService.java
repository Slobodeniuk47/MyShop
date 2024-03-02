package org.example.Infrastructure.google;

import com.google.api.client.googleapis.auth.oauth2.GoogleIdToken;
import com.google.api.client.googleapis.auth.oauth2.GoogleIdToken.Payload;
import com.google.api.client.googleapis.auth.oauth2.GoogleIdTokenVerifier;
import com.google.api.client.http.HttpTransport;
import com.google.api.client.http.javanet.NetHttpTransport;
import com.google.api.client.json.JsonFactory;
import com.google.api.client.json.jackson2.JacksonFactory;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.security.GeneralSecurityException;
import java.util.Arrays;
import java.util.Collections;


@Service
public class GoogleAuthService {
    private final HttpTransport transport = new NetHttpTransport();
    private final JsonFactory jsonFactory = new JacksonFactory();
    private final String CLIENT_ID = "85911906235-mpbk79c4do3jhbf2drgemm9q2n2sd6ca.apps.googleusercontent.com";


    public Payload verify(String idTokenString)
            throws GeneralSecurityException, IOException, InvalidTokenException {
        return verifyToken(idTokenString);
    }

    private Payload verifyToken(String idTokenString)
            throws GeneralSecurityException, IOException, InvalidTokenException {
        final GoogleIdTokenVerifier verifier = new GoogleIdTokenVerifier.
                Builder(transport, jsonFactory)
                .setIssuers(Arrays.asList("https://accounts.google.com", "accounts.google.com"))
                .setAudience(Collections.singletonList(CLIENT_ID))
                .build();


        System.out.println("validating:" + idTokenString);

        GoogleIdToken idToken = null;
        try {
            idToken = verifier.verify(idTokenString);
        } catch (IllegalArgumentException e){
            // means token was not valid and idToken
            // will be null
        }

        if (idToken == null) {
            throw new InvalidTokenException("idToken is invalid");
        }

        return idToken.getPayload();
    }
}
