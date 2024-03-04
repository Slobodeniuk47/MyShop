package org.example.Infrastructure.services;

import io.jsonwebtoken.*;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;
import io.jsonwebtoken.security.SignatureException;
import lombok.RequiredArgsConstructor;
import org.example.DAL.constants.Path;
import org.example.DAL.entities.account.UserEntity;
import org.example.DAL.repositories.IUserRoleRepository;
import org.springframework.stereotype.Service;
import io.jsonwebtoken.Claims;
import java.security.Key;
import java.util.Date;

import static java.lang.String.format;

@Service
@RequiredArgsConstructor
public class JwtTokenService {
//Working with  jjwt.*  0.11.5 verison
    private final IUserRoleRepository _userRoleRepository;
    private final String jwtSecret = "404E635266556A586E3272357538782F413F4428472B4B6250645367566B5970";  //the key we use to encrypt (any letters or numbers)
    private final String jwtIssuer = "step.io";   //indicates who owns this token. You can enter your domain name

    //Create token for user
    public String generateAccessToken(UserEntity user) {

        var roles = _userRoleRepository.findByUser(user); //get list Roles by User

        return Jwts.builder()
                .setSubject(format("%s,%s", user.getId(), user.getEmail()))
                .claim("id", user.getId())
                .claim("email", user.getEmail())
                .claim("firstname", user.getFirstname())
                .claim("lastname", user.getLastname())
                .claim("phoneNumber", user.getPhoneNumber())
                .claim("image", user.isGoogle() == false ? Path.ApiURL + "images/"+ user.getImage() : user.getImage())
                .claim("roles", roles.stream()                                      //writing the list Roles by User
                        .map((role) -> role.getRole().getName()).toArray(String []:: new))
                .setIssuer(jwtIssuer) //Writing the owner of the token
                .setIssuedAt(new Date(System.currentTimeMillis()))  //When was created token
                .setExpiration(new Date(System.currentTimeMillis() + 7 * 24 * 60 * 60 * 1000)) // 7 days (first value) lifetime of the token
                .signWith(getSignInKey(), SignatureAlgorithm.HS256)  //Encrypt the token using a secret key
                .compact();
    }


    private Key getSignInKey() {
        byte[] keyBytes = Decoders.BASE64.decode(jwtSecret);
        return Keys.hmacShaKeyFor(keyBytes);
    }

    // Get user.id from Token
    public String getUserId(String token) {
        Claims claims = Jwts.parser()
                .setSigningKey(jwtSecret)    // it is checked whether this token was issued by our server
                .parseClaimsJws(token)
                .getBody();

        return claims.getSubject().split(",")[0]; //takes the first element from the token Id
    }
    // Get user.username from Token
    public String getUsername(String token) {
        Claims claims = Jwts.parser()
                .setSigningKey(jwtSecret)
                .parseClaimsJws(token)
                .getBody();

        return claims.getSubject().split(",")[1];
    }
    // Get the token's expiration date
    public Date getExpirationDate(String token) {
        Claims claims = Jwts.parser()
                .setSigningKey(jwtSecret)
                .parseClaimsJws(token)
                .getBody();

        return claims.getExpiration();
    }
    //checks if our token is valid and issued by our server
    public boolean validate(String token) {
        try {
            Jwts.parser().setSigningKey(jwtSecret).parseClaimsJws(token);
            return true;
        } catch (SignatureException ex) {
            System.out.println("Invalid JWT signature - "+ ex.getMessage());
        } catch (MalformedJwtException ex) {
            System.out.println("Invalid JWT token - " + ex.getMessage());
        } catch (ExpiredJwtException ex) {
            System.out.println("Expired JWT token - " + ex.getMessage());
        } catch (UnsupportedJwtException ex) {
            System.out.println("Unsupported JWT token - " + ex.getMessage());
        } catch (IllegalArgumentException ex) {
            System.out.println("JWT claims string is empty - " + ex.getMessage());
        }
        return false;
    }
}