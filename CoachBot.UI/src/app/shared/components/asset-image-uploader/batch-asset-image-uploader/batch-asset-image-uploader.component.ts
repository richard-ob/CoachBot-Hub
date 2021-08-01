import { Component, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { AssetImage } from '@shared/models/asset-image.model';
import { AssetImageService } from '@shared/services/asset-image.service';
import { BatchAssetImageItem } from './batch-asset-image-item.model';

@Component({
    selector: 'app-batch-asset-image-uploader',
    templateUrl: './batch-asset-image-uploader.component.html',
    styleUrls: ['./batch-asset-image-uploader.component.scss']
})
export class BatchAssetImageUploaderComponent implements OnInit {
    @Output() imageUploaded = new EventEmitter<BatchAssetImageItem[]>();
    @Input() allowDirectImageAccess = true;
    files: BatchAssetImageItem[] = [];
    file: AssetImage;
    invalidImage = false;
    uploadSuccessful = false;
    isUploading = false;
    hasUploaded = true;
    PNG_BASE64_REGEX = new RegExp('^data:image//(?:png)(?:;charset=utf-8)?;base64,(?:[A-Za-z0-9]|[+//])+={0,2}');

    constructor(private assetImageService: AssetImageService) { }

    ngOnInit() {

    }

    fileSelected(event: any) {
        const file = event.target.files[0];
        const fileReader = new FileReader();
        this.isUploading = true;
        this.invalidImage = false;
        this.uploadSuccessful = false;
        fileReader.addEventListener('load', () => {
            this.file = {
                base64EncodedImage: fileReader.result,
                fileName: file.name,
                allowDirectAccess: this.allowDirectImageAccess
            };
            if (this.PNG_BASE64_REGEX.test(this.file.base64EncodedImage)) {
                this.invalidImage = true;
            } else {                
                this.uploadFile();
            }
        });
        fileReader.readAsDataURL(file);
        this.hasUploaded = false;
    }

    uploadFile() {
        this.uploadSuccessful = false;
        if (!this.invalidImage) {
            this.isUploading = true;
            this.assetImageService.createAssetImage(this.file).subscribe(assetImageId => {
                this.isUploading = false;
                this.uploadSuccessful = true;
                this.hasUploaded = true;
                this.files.push({ assetImageId, fileName: this.file.fileName });
                this.imageUploaded.emit(this.files);
                this.file = null;
            },
                error => {
                    this.invalidImage = true;
                    this.isUploading = false;
                    this.hasUploaded = false;
                }
            );
        }
    }

}
