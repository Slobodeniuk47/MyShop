package org.example.API.configuration.security;

import lombok.RequiredArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

@Configuration
@EnableWebSecurity
@RequiredArgsConstructor
public class SecurityConfig {
    private final AuthenticationProvider _authenticationProvider;
    private final JwtAuthenticationFilter _jwtAuthFilter;
//    @Autowired
//    @Bean
//    public static BCryptPasswordEncoder metodoCrittografia() {
//        return new BCryptPasswordEncoder();
//    }

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        http

                .cors(AbstractHttpConfigurer::disable)
                .csrf(AbstractHttpConfigurer::disable)
                .authorizeHttpRequests(auth-> auth
                        .requestMatchers("/").permitAll()
                        .requestMatchers("/api/Role/**").permitAll()
                        .requestMatchers("/api/Account/**").permitAll()
//                        .requestMatchers("/api/Account/edit").permitAll()
//                        .requestMatchers("/api/Account/login").permitAll()
//                        .requestMatchers("/api/Account/register").permitAll()
                        .requestMatchers("/images/**").permitAll()
                        .requestMatchers("/static/**").permitAll()
                        .requestMatchers("/swagger-resources/**").permitAll()
                        .requestMatchers("/v3/api-docs/**").permitAll()
                        .requestMatchers("/webjars/**").permitAll()
                        .requestMatchers("/rest-api-docs/**").permitAll()
                        .requestMatchers("/swagger-ui/**").permitAll()
                        .requestMatchers("/api/category/**").permitAll()//.hasAuthority(Roles.Admin)
                        .requestMatchers("/api/product/**").permitAll()//.hasAuthority(Roles.Admin)
                        .anyRequest().authenticated()
                )
                .sessionManagement(it->it.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
                .authenticationProvider(_authenticationProvider)
                .addFilterBefore(_jwtAuthFilter, UsernamePasswordAuthenticationFilter.class);

        return http.build();
    }
}