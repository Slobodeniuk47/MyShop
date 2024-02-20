package org.example.API.configuration.security;

import lombok.RequiredArgsConstructor;
import org.example.DAL.entities.UserEntity;
import org.example.DAL.repositories.UserRepository;
import org.example.DAL.repositories.UserRoleRepository;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.AuthenticationProvider;
import org.springframework.security.authentication.dao.DaoAuthenticationProvider;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.AuthorityUtils;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

import java.util.Collection;

@Configuration
@RequiredArgsConstructor
public class ApplicationConfig {

    private final UserRepository _userRepository;
    private final UserRoleRepository userRoleRepository;

    @Bean
    public UserDetailsService userDetailsService() {
        var userDetailService = new UserDetailsService() {
            @Override
            public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
                var userEntity = _userRepository.findByEmail(username).get();
                //Information about the user and a list of his roles
                var roles = getRoles(userEntity);
                var userDetails = new User(userEntity.getEmail(), userEntity.getPasswordHash(), roles);
                return userDetails; // if there is, then a new user is created based on what is in the database
            }
            private Collection<? extends GrantedAuthority> getRoles(UserEntity userEntity) {
                var roles = userRoleRepository.findByUser(userEntity);
                String [] userRoles = roles.stream()                                      //a list of roles that the user has is extracted
                        .map((role) -> role.getRole().getName()).toArray(String []:: new);
                Collection<GrantedAuthority> authorityCollections =                               //a new authority Collections collection is created
                        AuthorityUtils.createAuthorityList(userRoles);
                return authorityCollections;
            }
        };
        return userDetailService;
    }

    @Bean
    public AuthenticationProvider authenticationProvider() {
        DaoAuthenticationProvider authProvider = new DaoAuthenticationProvider();
        authProvider.setUserDetailsService(userDetailsService());
        authProvider.setPasswordEncoder(passwordEncoder());
        return authProvider;
    }

    @Bean
    public AuthenticationManager authenticationManager(AuthenticationConfiguration config) throws Exception {
        return config.getAuthenticationManager();
    }

    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }

}
