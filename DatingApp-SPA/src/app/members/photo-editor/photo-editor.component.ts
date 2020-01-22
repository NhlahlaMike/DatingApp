import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from '../../_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { AuthService } from '../../_services/auth.service';
import { environment } from '../../../environments/environment';
import { UserService } from '../../_services/user.service';
import { Observable } from 'rxjs';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() getMemberPhotoChange = new EventEmitter<string>();
  baseUrl = environment.apiUrl;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  currentMain: Photo;

constructor(private authService: AuthService,
            private userService: UserService,
            private alertify: AlertifyService) {}

ngOnInit() {
  this.initializeUploader();
}

initializeUploader() {
  this.uploader = new FileUploader({
    url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos',
    authToken: 'Bearer ' + localStorage.getItem('token'),
    isHTML5: true,
    allowedFileType: ['image'],
    removeAfterUpload: true,
    autoUpload: false,
    maxFileSize: 10 * 1024 * 1024
  });

  this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
  this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };

        if (photo.isMain) {
          this.authService.changeMemberPhotoUrl(photo.url);
          this.authService.currentUser.photoUrl = photo.url;
          localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
        }
        this.photos.push(photo);
      }
  };
}

fileOverBase(e: any): void {
  this.hasBaseDropZoneOver = e;
}

setMainPhoto(photo: Photo) {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
    this.currentMain = this.photos.filter(p => p.isMain === true)[0];  // [0] returns of 1 array element
    this.currentMain.isMain = false;
    photo.isMain = true;
    this.authService.changeMemberPhotoUrl(photo.url);
    // this.getMemberPhotoChange.emit(photo.url);
    this.authService.currentUser.photoUrl = photo.url;
    localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
  }, error => {
    this.alertify.error(error);
  });
}

deletePhoto(id) {
  this.alertify.confirm('Are you sure you want to delete', () => {
    this.userService.deletePhoto(this.authService.decodedToken.nameid, id).subscribe(() => {
      this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
      this.alertify.success('Photo has been deleted');
  }, error => {
    this.alertify.error('Failed to delete the photo');
    });
  });
}

}


