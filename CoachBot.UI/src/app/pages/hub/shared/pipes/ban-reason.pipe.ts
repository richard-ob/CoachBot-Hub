import { Pipe, PipeTransform } from '@angular/core';
import { BanReason, BanType } from '../model/ban.model';

@Pipe({ name: 'banReason' })
export class BanReasonPipe implements PipeTransform {
    transform(banReason: BanReason): string {
        switch (banReason) {
            case BanReason.Cheating:
                return $localize`:@@banReason.cheating:Cheating`;
            case BanReason.Harrassment:
                return $localize`:@@banReason.harrassment:Harrassment`;
            case BanReason.Homophobia:
                return $localize`:@@banReason.homophobia:Homophobia`;
            case BanReason.MatchmakingIssue:
                return $localize`:@@banReason.matchmakingIssue:Matchmaking Issue`;
            case BanReason.Other:
                return $localize`:@@banReason.other:Other`;
            case BanReason.Racism:
                return $localize`:@@banReason.racism:Racism`;
            case BanReason.Sexism:
                return $localize`:@@banReason.sexism:Sexism`;
            case BanReason.RageQuit:
                return $localize`:@@banReason.rageQuit:Rage Quit`;
            default:
                return '';
        }
    }
}
