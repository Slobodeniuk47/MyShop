package org.example.Infrastructure.services;

import lombok.AllArgsConstructor;
import org.example.DAL.entities.CommentEntity;
import org.example.DAL.entities.CommentImageEntity;
import org.example.DAL.repositories.ICommentImageRepository;
import org.example.DAL.repositories.ICommentRepository;
import org.example.DAL.repositories.IProductRepository;
import org.example.DAL.repositories.IUserRepository;
import org.example.Infrastructure.dto.commentDTO.CommentCreateDTO;
import org.example.Infrastructure.dto.commentDTO.CommentEditDTO;
import org.example.Infrastructure.dto.commentDTO.CommentItemDTO;
import org.example.Infrastructure.interfaces.IServices.ICommentService;
import org.example.Infrastructure.interfaces.IMappers.ICommentMapper;
import org.example.Infrastructure.storage.IStorageService;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

@Service
@AllArgsConstructor
public class CommentService implements ICommentService {
    private final IStorageService _storageService;
    private final ICommentMapper _commentMapper;
    private final ICommentRepository _commentRepository;
    private final ICommentImageRepository _commentImageRepository;
    private final IUserRepository _userRepository;
    private final IProductRepository _productRepository;

    public CommentItemDTO create(CommentCreateDTO model) {
        var comment = new CommentEntity();
        comment.setTitle(model.getTitle());
        comment.setMessage(model.getMessage());
        comment.setStars(model.getStars());
        comment.setUser(_userRepository.getById(model.getUserId()));
        comment.setProduct(_productRepository.getById(model.getProductId()));
        _commentRepository.save(comment);
        for (var img : model.getImages()) {
            var file = _storageService.saveMultipartFile(img);
            CommentImageEntity pi = new CommentImageEntity();
            pi.setName(file);
            pi.setComment(comment);
            _commentImageRepository.save(pi);
        }
        return null;
    }
    public List<CommentItemDTO> getAll(){
        var comments = _commentRepository.findAll();
        return _commentRepository.saveAll(comments)
                .stream()
                .map(_commentMapper::CommentItemDTOByCommentEntity)
                .collect(Collectors.toList());
    }
    public CommentItemDTO edit(CommentEditDTO model){
        var commentOptional = _commentRepository.findById(model.getId());
        if(commentOptional.isPresent()) {
            var comment = commentOptional.get();
            comment.setTitle(model.getTitle());
            comment.setMessage(model.getMessage());
            comment.setStars(model.getStars());
            comment.setUser(_userRepository.getById(model.getUserId()));
            comment.setProduct(_productRepository.getById(model.getProductId()));
            comment.setDateUpdated(LocalDateTime.now());
            _commentRepository.save(comment);

            if(!Objects.isNull(model.getImages())) {
                //Delete photos
                for (CommentImageEntity existingImage : _commentImageRepository.findAll()) {
                    if(existingImage.getComment().getId() == commentOptional.get().getId()) {
                        _storageService.removeFile(existingImage.getName());
                        _commentImageRepository.delete(existingImage);
                    }
                }
                //Save photos
                for (var img : model.getImages()) {
                    var file = _storageService.saveMultipartFile(img);
                    CommentImageEntity pi = new CommentImageEntity();
                    pi.setName(file);
                    pi.setComment(comment);
                    _commentImageRepository.save(pi);
                }
            }
        }
        return null;
    }
    public void deleteById(int id){
        CommentEntity product = _commentRepository.findById(id).get();
        for(var img : product.getCommentImages()) {
            _storageService.removeFile(img.getName());
        }
        _commentRepository.deleteById(id);
    }
    public CommentItemDTO getById(int id){
        var commentOptional = _commentRepository.findById(id);
        if(commentOptional.isPresent())
        {
            var result = _commentMapper.CommentItemDTOByCommentEntity(commentOptional.get());
            return result;
        }
        return null;
    }
    public List<CommentItemDTO> getCommentsByProductIdAsync(int id){
        var commentsByProductId = new ArrayList<CommentEntity>();
        for (var comment : _commentRepository.findAll()) {
            if(comment.getProduct().getId() == id) {
                commentsByProductId.add(comment);
            }
        }
        return _commentRepository.saveAll(commentsByProductId)
                .stream()
                .map(_commentMapper::CommentItemDTOByCommentEntity)
                .collect(Collectors.toList());
    }
}
