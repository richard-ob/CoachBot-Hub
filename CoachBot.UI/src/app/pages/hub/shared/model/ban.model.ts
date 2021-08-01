import { Player } from "./player.model";

export interface Ban {
    id?: number;
    banType?: BanType;
    banReason?: BanReason;
    banInfo?: string;
    startDate?: Date;
    endDate?: Date;
    sourceBansId?: number;
    bannedPlayerId?: number;
    bannedPlayer?: Player;
    createdDate?: Date;
    createdBy?: Player;
    updatedDate?: Date;
    updatedBy?: Player;
}

export enum BanType {
    Matchmaking,
    Community
}

export enum BanReason {
    Cheating,
    Harrassment,
    Racism,
    Homophobia,
    Sexism,
    RageQuit,
    MatchmakingIssue,
    Other
}
