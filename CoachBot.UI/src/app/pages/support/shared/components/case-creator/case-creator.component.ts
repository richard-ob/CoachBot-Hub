import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BatchAssetImageItem } from '@shared/components/asset-image-uploader/batch-asset-image-uploader/batch-asset-image-item.model';
import { FileSystemDirectoryEntry, FileSystemFileEntry, NgxFileDropEntry } from 'ngx-file-drop';
import { CaseType } from '../../case-type.enum';
import { Case } from '../../case.model';
import { CaseService } from '../../case.service';
import { CreateCaseDto } from './create-case-dto.interface';

@Component({
    selector: 'app-case-creator',
    templateUrl: './case-creator.component.html',
    styleUrls: ['./case-creator.component.scss']
})
export class CaseCreatorComponent implements OnInit {

    isLoading = true;
    isCreating = false;
    captchaResolved = false;
    caseTypes = CaseType;
    newCase: CreateCaseDto;

    constructor(private caseService: CaseService, private router: Router) { }

    ngOnInit(): void {
        this.newCase = {
            title: '',
            description: '',
            caseType: null,
            images: []
        };
        this.isLoading = false;
    }

    createCase(): void {
        this.isCreating = true;
        this.caseService.createCase(this.newCase).subscribe((caseId) => {
            this.isCreating = false;
            this.router.navigate(['/support/' + caseId]);
        });
    }

    setCaptchaResolved(): void {
        this.captchaResolved = true;
    }

    imageUploaded(files: BatchAssetImageItem[]) {
        this.newCase.images = files.map(f => f.assetImageId);
    }
  
}
