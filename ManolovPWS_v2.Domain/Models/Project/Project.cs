using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using ManolovPWS_v2.Domain.Models.Project.Properties;

namespace ManolovPWS_v2.Domain.Models.Project
{
    public sealed class Project : IEntity<ProjectId>
    {
        public ProjectId Id { get; }
        public Guid OwnerId { get; }
        public ProjectName Name { get; }
        public ProjectDescription Description { get; }
        public ProjectState State { get; private set; }
        public ProjectLiveUrl? LiveUrl { get; }
        public ProjectGitHubUrl? GitHubUrl { get; }
        public ProjectUploadedDate UploadedDate { get; init; }
        public ProjectUpdatedDate? UpdatedDate { get; }
        public ProjectGallery Gallery { get; }
        public ProjectPicture Thumb { get; }

#pragma warning disable S107 // Constructor has too many parameters
        private Project(
            ProjectId id,
            Guid ownerId,

            ProjectName name,
            ProjectDescription description,
            ProjectState state,

            ProjectPicture thumb,
            ProjectUploadedDate uploadedDate,
            ProjectUpdatedDate? updatedDate = default,

            ProjectLiveUrl? liveUrl = default,
            ProjectGitHubUrl? gitHubUrl = default,
            ProjectGallery? gallery = default
            )
        {
            Id = id; 
            OwnerId = ownerId;
            Name = name;
            Description = description;
            State = state;
            LiveUrl = liveUrl;
            GitHubUrl = gitHubUrl;
            UploadedDate = uploadedDate;
            UpdatedDate = updatedDate;
            Gallery = gallery ?? ProjectGallery.Empty();
            Thumb = thumb;
        }

        private Project With(
            ProjectName? name = default,
            ProjectDescription? description = default,
            ProjectState? state = default,

            ProjectLiveUrl? liveUrl = default,
            ProjectGitHubUrl? gitHubUrl = default,

            ProjectPicture? thumb = default,
            ProjectGallery? gallery = default
            )
        {
            var updateDate = ProjectUpdatedDate.Create(DateOnly.FromDateTime(DateTime.UtcNow));

            ValidateUpdateDate(UploadedDate, updateDate);

            return new(
                Id,
                OwnerId,
                name ?? Name,
                description ?? Description,
                state ?? State,
                thumb ?? Thumb,
                UploadedDate,
                updateDate,
                liveUrl ?? LiveUrl,
                gitHubUrl ?? GitHubUrl,
                gallery ?? Gallery
                );
        }

        public static Project Create(
            ProjectId id,
            Guid ownerId,

            ProjectName name,
            ProjectDescription description,
            ProjectState state,

            ProjectPicture thumb,
            ProjectUploadedDate uploadedDate,
            ProjectUpdatedDate? updatedDate = default,

            ProjectLiveUrl? liveUrl = default,
            ProjectGitHubUrl? gitHubUrl = default,
            ProjectGallery? gallery = default
            )
        {
            ValidateUpdateDate(uploadedDate, updatedDate);

            return new(
                id,
                ownerId,
                name,
                description,
                state,
                thumb,
                uploadedDate,
                updatedDate,
                liveUrl,
                gitHubUrl,
                gallery
                );
        }
        
        // Project manipulations
        public Project UpdateName(ProjectName newName)
        {
            if (Name.Equals(newName)) return this;

            return With(name: newName);
        }
        public Project UpdateDescription(ProjectDescription newDescription)
        {
            if (Description.Equals(newDescription)) return this;

            return With(description: newDescription);
        }
        public Project UpdateState(ProjectState newState)
        {
            if (State.Equals(newState)) return this;

            return With(state: newState);
        }
        public Project UpdateThumb(ProjectPicture newThumb)
        {
            if (Thumb.Equals(newThumb)) return this;

            return With(thumb: newThumb);
        }
        public Project UpdateLiveUrl(ProjectLiveUrl newLiveUrl)
        {
            if (LiveUrl is not null && LiveUrl.Equals(newLiveUrl)) return this;

            return With(liveUrl: newLiveUrl);
        }
        public Project UpdateGitHubUrl(ProjectGitHubUrl newGitHubUrl)
        {
            if (GitHubUrl is not null && GitHubUrl.Equals(newGitHubUrl)) return this;

            return With(gitHubUrl: newGitHubUrl);
        }

        // Gallery manipulations
        public Project ClearGallery()
            => With(gallery: ProjectGallery.Empty());

        public Project ReplaceGallery(ProjectGallery newGallery)
        {
            if (Gallery.Equals(newGallery)) return this;
            
            return With(gallery: newGallery);
        }
        public Project AddToGallery(ProjectPicture newPicture)
        {
            var updated = this.Gallery.AddPicture(newPicture);

            if (Gallery.Equals(updated)) return this;

            return With(gallery: updated);
        }
        public Project AddToGallery(IEnumerable<ProjectPicture> newPictures)
        {
            var updated = this.Gallery.AddPictures(newPictures);

            if (Gallery.Equals(updated)) return this;

            return With(gallery: updated);
        }
        public Project RemoveFromGallery(ProjectPicture pictureToRemove)
        {
            var updated = this.Gallery.RemovePicture(pictureToRemove);

            if (Gallery.Equals(updated)) return this;

            return With(gallery: updated);
        }
        public Project RemoveFromGallery(IEnumerable<ProjectPicture> picturesToRemove)
        {
            var updated = this.Gallery.RemovePictures(picturesToRemove);

            if (Gallery.Equals(updated)) return this;

            return With(gallery: updated);
        }

        // Validations
        private static void ValidateUpdateDate(ProjectUploadedDate uploaded, ProjectUpdatedDate? updated)
        {
            if (updated is not null && updated.Value < uploaded.Value)
                throw new InvalidProjectDateException("Updated date cannot be earlier than uploaded date.");
        }
    }
}
