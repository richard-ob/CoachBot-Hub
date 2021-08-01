import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SpinnerModule } from 'src/app/core/components/spinner/spinner.module';
import { AssetImageUploaderComponent } from './asset-image-uploader.component';
import { BatchAssetImageUploaderComponent } from './batch-asset-image-uploader/batch-asset-image-uploader.component';
import { NgxFileDropModule } from 'ngx-file-drop';

@NgModule({
    declarations: [
        AssetImageUploaderComponent,
        BatchAssetImageUploaderComponent
    ],
    exports: [
        AssetImageUploaderComponent,
        BatchAssetImageUploaderComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        SpinnerModule,
        NgxFileDropModule,
        SpinnerModule
    ]
})
export class AssetImageUploaderModule { }
