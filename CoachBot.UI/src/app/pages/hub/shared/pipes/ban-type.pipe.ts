import { Pipe, PipeTransform } from '@angular/core';
import { BanType } from '../model/ban.model';

@Pipe({ name: 'banType' })
export class BanTypePipe implements PipeTransform {
    transform(banType: BanType): string {
        switch (banType) {
            case BanType.Community:
                return $localize`:@@banTypes.community:Community`;
            case BanType.Matchmaking:
                return $localize`:@@banTypes.matchmaking:Matchmaking`;
            default:
                return '';
        }
    }
}
